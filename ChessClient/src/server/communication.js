var net = require('net');

var client = new net.Socket();

function connect(read_callback) {
    client.connect(3030, '127.0.0.1', function() {
        console.log('Connected');
    });

    client.on('data', function(data) {
        console.log('Received: ' + data);
        read_callback(data)
            // client.destroy();
    });
}

function send(data) {
    client.write('Hello, server! Love, Client.');
}

client.on('close', function() {
    console.log('Connection closed');
});