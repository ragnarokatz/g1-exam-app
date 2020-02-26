// ################################################################################
// Web service setup

require("dotenv").config();

const express = require("express");
const cors = require("cors");
const path = require("path");
const bodyParser = require("body-parser");
const app = express();
const HTTP_PORT = process.env.PORT || 8080;
// Or use some other port number that you like better

// Add support for incoming JSON entities
app.use(bodyParser.json());
// Add support for CORS
app.use(cors());

// ################################################################################
// Data model and persistent store setup

const manager = require("./manager.js");
const m = manager();

// ################################################################################
// Deliver the app's home page to browser clients

app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "/index.html"));
});

// ################################################################################
// Resources available in this web API

app.get("/api", (req, res) => {
  // Here are the resources that are available for users of this web API...
  const links = [];
  // This app's resources...
  links.push({ rel: "collection", href: "/api/questions", methods: "GET" });
  links.push({
    rel: "collection",
    href: "/api/questions/:id",
    methods: "GET"
  });

  const linkObject = {
    apiName: "G1 Exam Web API",
    apiDescription: "Ontario G1 Practise Exam Web API Services",
    apiVersion: "1.0",
    apiAuthor: "Bowei Yao",
    links: links
  };
  res.json(linkObject);
});

// ################################################################################
// Request handlers for data entities (listeners)

// Get all
app.get("/api/questions", (req, res) => {
  // Call the manager method
  m.questionGetAll(req.query.page)
    .then(data => {
      res.json(data);
    })
    .catch(error => {
      res.status(500).json({ message: error });
    });
});

// Get one
app.get("/api/questions/:id", (req, res) => {
  // Call the manager method
  m.questionGetById(req.params.id)
    .then(data => {
      res.json(data);
    })
    .catch(() => {
      res.status(404).json({ message: "Resource not found" });
    });
});

// ################################################################################
// Resource not found (this should be at the end)

app.use((req, res) => {
  res.status(404).send("Resource not found");
});

// ################################################################################
// Attempt to connect to the database, and
// tell the app to start listening for requests

m.connect()
  .then(() => {
    app.listen(HTTP_PORT, () => {
      console.log("Ready to handle requests on port " + HTTP_PORT);
    });
  })
  .catch(err => {
    console.log("Unable to start the server:\n" + err);
    process.exit();
  });
