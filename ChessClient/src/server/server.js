const express = require('express')
const cookieParser = require("cookie-parser");
const session = require('express-session');
const uuidv4 = require("uuid").v4

const client_communication = require('./client_communication');

const app = express()

const cookieAgeMs = 1000 * 60 * 10;

//session middleware
const sessionMiddleware = session({
    secret: "a9s8dasdn89auyv9n3y5v3y5bvh8945u6j",
    saveUninitialized: true,
    cookie: { maxAge: cookieAgeMs },
    resave: true
});
app.use(sessionMiddleware);

app.use(cookieParser());

app.use(express.static('dist'));

let listenPort = process.env.PORT || 80

const server = app.listen(listenPort, function() {
    printListening(server)
})

client_communication.startSocketIo(server, sessionMiddleware)

function printListening(server) {
    let host = server.address().address;
    let port = server.address().port;
    console.log("Listening at http://%s:%s", host, port);
}