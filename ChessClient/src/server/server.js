const express = require('express')
const cookieParser = require("cookie-parser");
const sessions = require('express-session');
const uuidv4 = require("uuid").v4

const client_communication = require('./client_communication');

const app = express()

app.use(express.static('dist'));

const cookieAgeMs = 1000 * 10

//session middleware
app.use(sessions({
    secret: "a9s8dasdn89auyv9n3y5v3y5bvh8945u6j",
    saveUninitialized: true,
    cookie: { maxAge: cookieAgeMs },
    resave: false
}));

app.use(cookieParser());

// sessionClients = {}

// app.get('/api/connect', (req, res) => {
//     const session = req.session
//     let userId = session.userId

//     if (!userId) {
//         userId = uuidv4()
//         session.userId = userId
//     }
//     if (!(session.userId in sessionClients) || !sessionClients[session.userId]) {
//         var readCallback = (data) => { console.log(data) }
//         client = communication.connect(readCallback)

//         let clientEndInterval = setInterval(() => { client.destroy() }, cookieAgeMs);

//         client.on('close', function() {
//             console.log('Connection closed');
//             sessionClients[session.userId] = undefined
//             clearInterval(clientEndInterval)
//         });
//         sessionClients[session.userId] = client
//     }
//     res.status(200)
//     res.end()
// })

// app.get('/api/session', (req, res) => {
//     const session = req.session

//     console.log(session.userId)
//     res.status(200)
//     res.end()
// })

// app.get('/api/send', (req, res) => {
//     const session = req.session
//     const client = sessionClients[session.userId]

//     if (!client) {
//         res.status(400)
//         res.end()
//         return
//     }

//     communication.sendJson(client, { "message": "OKKKKKK" })
//     res.status(200)
//     res.end()
// })

let listenPort = process.env.PORT || 80

const server = app.listen(listenPort, function() {
    printListening(server)
})

client_communication.startSocketIo(server)

function printListening(server) {
    let host = server.address().address;
    let port = server.address().port;
    console.log("Listening at http://%s:%s", host, port);
}