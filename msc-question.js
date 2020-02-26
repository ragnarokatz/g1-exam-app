// Setup
var mongoose = require("mongoose");
var Schema = mongoose.Schema;

// Entity schema

var questionSchema = new Schema({
  Question: String,
  Picture: String,
  Answers: Array,
  Correct: Number,
  Image: String
});

// Make schema available to the application
module.exports = questionSchema;
