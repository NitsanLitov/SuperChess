const express = require('express')
const communication = require('./communication.js')
const app = express()
const port = 80

app.use(express.static('src/client'))

app.get('/api/connect', (req, res) => {})


// app.get('/api/connect', (req, res) => {
//     const malId = req.query["mal_id"]
//     const watchedEpisodes = Number(req.query["watched_episodes"])
//     const setCompleted = Boolean(req.query["set_completed"])
//     const username = req.query["username"]

//     if (!malId) {
//         res.end(`mal_id is mandatory`)
//         return
//     }
//     if (!watchedEpisodes) {
//         res.end(`watched_episodes is mandatory and supposed to be a number`)
//         return
//     }
//     if (!username) {
//         res.end(`username is mandatory`)
//         return
//     }

//     handleMalApiResult(mal.updateWatchedEpisodes(username, malId, watchedEpisodes, setCompleted), res)
// })

app.listen(port, () => {})

communication.connect((data) => { console.log(`got ${data}`) })
communication.send("asdawezxc")