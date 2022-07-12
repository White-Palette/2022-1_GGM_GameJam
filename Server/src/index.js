import * as ws from 'ws';
import { v4 } from 'uuid';

const server = new ws.WebSocketServer({ port: 3002 });

const clients = [];
let count = 0;

function broadcast(data) {
    server.clients.forEach(client => {
        client.send(data);
    });
}

function buildRealtimeLeaderboardPacket() {
    const leaderboard = clients.map(client => {
        return {
            i: client.id,
            h: client.height
        };
    });

    leaderboard.sort((a, b) => {
        return b.height - a.height;
    });

    return JSON.stringify({
        t: 'li',
        p: JSON.stringify({
            l: leaderboard
        })
    });
}

function buildJoinPacket(client) {
    return JSON.stringify({
        t: 'j',
        p: JSON.stringify({
            i: client.id,
            n: client.name,
            c: client.color
        })
    });
}

function buildLeavePacket(client) {
    return JSON.stringify({
        t: 'l',
        p: JSON.stringify({
            i: client.id
        })
    });
}

server.on('listening', () => {
    console.log('Server is listening on port 3002');
});

server.on('connection', socket => {
    socket.id = count++;

    setInterval(() => {
        socket.ping();
    }, 5000);

    socket.send(JSON.stringify({
        t: 'i',
        p: socket.id
    }));

    socket.on('message', message => {
        const data = JSON.parse(message);

        switch (data.t) {
            case 'n': {
                if (clients.find(client => client.id === socket.id)) {
                    return;
                }

                socket.name = data.p;
                socket.height = 0;
                socket.color = '#' + Math.floor(Math.random() * 16777215).toString(16);

                clients.forEach(client => {
                    socket.send(buildJoinPacket(client));
                });

                clients.push(socket);
                broadcast(buildJoinPacket(socket));

                break;
            }
            case 'h': {
                socket.height = data.p;
                broadcast(buildRealtimeLeaderboardPacket());
                break;
            }
        }
    });

    socket.on('close', () => {
        clients.splice(clients.indexOf(socket), 1);
        broadcast(buildLeavePacket(socket));
    });
});
