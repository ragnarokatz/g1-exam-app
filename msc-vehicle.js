// Setup
var mongoose = require("mongoose");
var Schema = mongoose.Schema;

// Entity schema

var vehicleSchema = new Schema({
  car_make: String,
  car_model: String,
  car_year: Number,
  vin: String,
  msrp: Number,
  photo: String,
  purchase_date: Date,
  purchaser_name: String,
  purchaser_email_address: String,
  price_paid: Number,
  dealer_address: String,
  dealer_name: String,
  dealer_phone: String
});

// Make schema available to the application
module.exports = vehicleSchema;
