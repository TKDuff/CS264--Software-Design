/*CRUD for phone and customer information */

const {MongoClient} = require('mongodb');
const uri = "mongodb+srv://thomaskilduff:@cluster0.wns9h.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";   //authentication details
const client = new MongoClient(uri);


/*Arrays for random customer and phone data to be picked from */
var FirstNames = ["Liam", "Olivia","Noah" , "Emma", "Oliver",  "Ava", "Elijah",  "Charlotte", "William", "Sophia", "James", "Amelia","Benjamin",  "Isabella"];
var Surnames = ["Smith", "White", "Leonard", "Bloggs", "Caffery", "Duff", "Doe", "Quinn", "Pearse", "Shay", "Dunne", "Murphy", "Ryan"];
var Titles = ["Mx", "Ms", "Mr", "Mrs", "Miss", "Dr"];
var Towns = ["Athlone","Dunboyne","Milltown","Delvin","Mullingar"];

var t = "Ms";
var fn = "Jane";
var sn = "Doe";
var ph = "083123456"
var em = "Jane@gmail.ie";
var twn = "N981F2";

/* These 3 methods pick a random value from the indexes to store in the variable*/
//Gets random firstname and surname for customer using arrays from above
function randomName() {
    fn = FirstNames[Math.floor(Math.random()*FirstNames.length)];
    sn = Surnames[Math.floor(Math.random()*Surnames.length)];
}

//Gets random title, phone number, email and town for customer
function randomUpdate(){
    t = Titles[Math.floor(Math.random()*Titles.length)];
    ph = "08" + String(parseInt(Math.random()*100000000, 10));    //primary key
    em = fn+"."+sn+"@mumail.ie"
    twn = Towns[Math.floor(Math.random()*Towns.length)];
}

//Creates random information
function randomPhone() {
    ma = Manufacturer[Math.floor(Math.random()*Manufacturer.length)];
    mo = Model[Math.floor(Math.random()*Model.length)];
    price = String(Math.floor(Math.random() * 400) + 100)
}

async function main(){
	try {
        // Connect to the MongoDB cluster
        await client.connect();
        console.log("connected");

        /*CRUD for customer */
        /*Create*/
        randomName();       
        randomUpdate();     //Create random customer
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

        /*CRUD for Item information and Order details*/
        randomPhone();      //create random phone values
        /*Create*/
        await insertPhone( 
            {
                "Manufacturer": ma,
                "Model": mo,
                "Price": price
            }
        ); 


        /*Find random phone and Updates the price (also updates price in customer order information of phone)*/
        findPhone(function(Price){
            price = String(Math.floor(Math.random() * 400) + 100)
            console.log("\nU:Changing the price of" + Price.substring(Price.indexOf(' ')) + " to €" + price);
            updatePrice(Price.substring(0, Price.indexOf(' ')), {"Price": price});      //method call to update phone
        }); 


        /*Delete random phone*/
        random(function(Price){
            console.log("\nD: Deleting phone" + Price.substring(Price.indexOf(' ')));    
            deletePhone(Price.substring(0, Price.indexOf(' ')));
        }); 


    } catch (e) {
        console.error(e);
    }

}

main().catch(console.error);

/* CRUD for Customer*/

/*Create*/
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






/*CRUD for phone items*/

/*Deletes random phone and if phone exists as customer order that order is deleted aswell*/
async function deletePhone(nameOfListing) {
    await client.db("sample").collection("ItemInformation").deleteOne({ Price: nameOfListing });
}

//Select random phone
async function random(callback) {
    MongoClient.connect(uri, function(err, db){
        db.db("sample").collection("ItemInformation").aggregate([{$sample: {size: 1}}]).toArray(function(err, result){      //use aggregate to pick random person from database
            db.close();
            return callback(result[0].Price + " " + result[0].Manufacturer + " " + result[0].Model); 
            
        });
    });
} 

/*Finds random phone*/
async function findPhone(callback) {
    MongoClient.connect(uri, function(err, db){
        db.db("sample").collection("ItemInformation").aggregate([{$sample: {size: 1}}]).toArray(function(err, result){
            db.close();
            var obj = result[0];
            console.log("\nR:Retrieving Phone: " + obj.Manufacturer + " " + obj.Model + " which costs: €" + obj.Price);
            return callback(obj.Price + " " + obj.Manufacturer + " " + obj.Model);            
        });
    });
}

/*Update*/
async function updatePrice(nameOfListing, updatedListing) {
    await client.db("sample").collection("ItemInformation").updateOne({ Price: nameOfListing }, { $set: updatedListing });      //updates price in "ItemInformation"
}

/*Create, inserts new phone into ItemInformation collection*/
async function insertPhone(info){
    const result = await client.db("sample").collection("ItemInformation").insertOne(info);
    console.log("C: Creating a new phone item\nManufacturer: " + info.Manufacturer + "\nModel: " + info.Model + "\nPrice: €" + info.Price);
}