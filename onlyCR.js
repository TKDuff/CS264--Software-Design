const {MongoClient} = require('mongodb');
const uri = "mongodb+srv://thomaskilduff:@cluster0.wns9h.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";   //authentication details
const client = new MongoClient(uri);


var t = "Ms";
var fn = "Jane";
var sn = "Doe";
var ph = "083123456"
var em = "Jane@gmail.ie";
var twn = "N981F2";



async function main(){
	try {
        // Connect to the MongoDB cluster
        await client.connect();
        console.log("connected");

        /*Create*/
        await insertCustomer(   //insert customer into db
            {
                "Title": t,
                "firstName": fn,
                "Surname": sn,
                "mobile": ph,
                "email": em,
                "PersonalAddress": 
                {
                    "Line1": "55",
                    "Line2": "Brookfield",
                    "Town": twn,
                    "County": "Westmeath",
                    "Eircode": "N91"
                },
                "ShippingAddress":
                {
                    "Line1": "65",
                    "Line2": "Brookfield",
                    "Town": twn,
                    "County": "Westmeath",
                    "Eircode": "N91"   
                }
            }
        );


        /*Retrieve & Update random customer*/
        findCustomer(function(mobile){      //first part finds customer, then we update this same customer
            randomUpdate();                 //create new information
            console.log("\nU: Updating" + mobile.substring(mobile.indexOf(' ')) + " information\nNew Title: " + t +  "\nNew phone number: " + ph + "\nNew email: " + em + "\nNew town they moved to: " + twn);
            updateCustomer(mobile.substring(0, mobile.indexOf(' ')), {"mobile": ph, "Title": t, "email": em, "PersonalAddress.Town": twn});//call function to update customer information with new values
        });

        /*Delete random customer*/
        randomCustomer(function(name){
            console.log("\nD: Deleting customer with the name " + name );    
            deleteCustomer(name);
        }); 


    } catch (e) {
        console.error(e);
    }

}

main().catch(console.error);

async function insertCustomer(info){   //Takes in random customer information
    const result = await client.db("sample").collection("PersonalDetails").insertOne(info);       //uploads this information
    console.log("C: Creating a new customer\nTitle: " + info.Title + "\nName: " + info.firstName + " " + info.Surname + "\nMobile: " + info.mobile + "\nEmail: " + info.email + 
        "\n\nHome address: " + info.PersonalAddress.Line1 + " " + info.PersonalAddress.Line2 + "\n              " + info.PersonalAddress.Town + "\n              " + info.PersonalAddress.County + "\n              " + info.PersonalAddress.Eircode +
        "\n\nShipping address: " + info.ShippingAddress.Line1 + " " + info.ShippingAddress.Line2 + "\n                  " + info.ShippingAddress.Town + "\n                  " + info.ShippingAddress.County + "\n                  " + info.PersonalAddress.Eircode);
}

/*Finds fandom person from database */
async function findCustomer(callback) {
    MongoClient.connect(uri, function(err, db){
        db.db("sample").collection("PersonalDetails").aggregate([{$sample: {size: 1}}]).toArray(function(err, result){     //use aggregate to pick random person from database
            db.close();
            var obj = result[0];
            console.log("\nR:Retrieving " + obj.firstName + " " + obj.Surname + " details\nTitle: " + obj.Title  + "\nMobile: " + obj.mobile + "\nEmail: " + obj.email + "\nLives in the town of " + obj.PersonalAddress.Town);
            return callback(obj.mobile + " " + obj.firstName + " " + obj.Surname);            
        });
    });
}

/*Update*/
async function updateCustomer(nameOfListing, updatedListing) {
    await client.db("sample").collection("PersonalDetails").updateOne({ mobile: nameOfListing }, { $set: updatedListing });
}

/*Deletes random customer from collection*/
async function deleteCustomer(nameOfListing) {
    await client.db("sample").collection("PersonalDetails").deleteOne({ firstName: nameOfListing.substring(0, nameOfListing.indexOf(' ')) });
}