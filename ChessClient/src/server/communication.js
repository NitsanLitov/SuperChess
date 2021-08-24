var net = require('net');

function connect(read_callback) {
    var client = new net.Socket();
    client.connect(3030, '127.0.0.1', function() {
        console.log('Connected');
    });

    client.on('error', function(e) {
        console.log("handled error");
        console.log(e);
    });

    client.on('data', function(data) {
        console.log('Received: ' + data);
        read_callback(JSON.parse(data))
    });

    return client
}

function sendJson(client, json) {
    return client.write(JSON.stringify(json));
}

module.exports = { connect, sendJson }