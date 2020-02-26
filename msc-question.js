// Setup
var mongoose = require("mongoose");
var Schema = mongoose.Schema;

// Entity schema

var questionSchema = new Schema({
  order: Number,
  question: String,
  picture: String,
  answers: Array,
  correct: Number,
  image: String
});

// Make schema available to the application
module.exports = questionSchema;
