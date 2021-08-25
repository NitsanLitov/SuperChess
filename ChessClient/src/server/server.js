const express = require('express')
const communication = require('./communication.js')
const cookieParser = require("cookie-parser");
const sessions = require('express-session');
const uuidv4 = require("uuid").v4

const app = express()
const port = 80

app.use(express.static('src/client'))

const cookieAgeMs = 1000 * 10

//session middleware
app.use(sessions({
    secret: "a9s8dasdn89auyv9n3y5v3y5bvh8945u6j",
    saveUninitialized: true,
    cookie: { maxAge: cookieAgeMs },
    resave: false
}));

app.use(cookieParser());

sessionClients = {}

app.get('/api/connect', (req, res) => {
    const session = req.session
    let userId = session.userId

    if (!userId) {
        userId = uuidv4()
        session.userId = userId
    }
    if (!(session.userId in sessionClients) || !sessionClients[session.userId]) {
        var readCallback = (data) => { console.log(data) }
        client = communication.connect(readCallback)

        let clientEndInterval = setInterval(() => { client.destroy() }, cookieAgeMs);

        client.on('close', function() {
            console.log('Connection closed');
            sessionClients[session.userId] = undefined
            clearInterval(clientEndInterval)
        });
        sessionClients[session.userId] = client
    }
    res.status(200)
    res.end()
})

app.get('/api/session', (req, res) => {
    const session = req.session

    console.log(session.userId)
    res.status(200)
    res.end()
})

app.get('/api/send', (req, res) => {
    const session = req.session
    const client = sessionClients[session.userId]

    if (!client) {
        res.status(400)
        res.end()
        return
    }

    communication.sendJson(client, { "message": "OKKKKKK" })
    res.status(200)
    res.end()
})

app.listen(port, () => {})