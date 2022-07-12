import * as ws from 'ws';
import { v4 } from 'uuid';

const server = new ws.WebSocketServer({ port: 3002 });

const clients = [];

function broadcast(data) {
    server.clients.forEach(client => {
        client.send(data);
    });
}

function getRealtimeLeaderboardPacket() {
    const leaderboard = clients.map(client => {
        return {
            i: client.id,
            n: client.name,
            h: client.height
        };
    });

    leaderboard.sort((a, b) => {
        return b.height - a.height;
    });

    return JSON.stringify({
        t: 'l',
        p: leaderboard
    });
}

server.on('listening', () => {
    console.log('Server is listening on port 3002');
});

server.on('connection', socket => {
    socket.id = v4();

    socket.send(JSON.stringify({
        t: 'i',
        p: socket.id
    }));

    socket.on('message', message => {
        const data = JSON.parse(message);

        switch (data.type) {
            case 'name': {
                if (clients.find(client => client.name === data.name)) {
                    return;
                }

                socket.name = data.payload;
                clients.push(socket);
                break;
            }
            case 'height': {
                socket.height = data.payload;
                broadcast(getRealtimeLeaderboardPacket());
                break;
            }
        }
    });

    socket.on('close', () => {
        clients.splice(clients.indexOf(socket), 1);
        broadcast(getRealtimeLeaderboardPacket());
    });
});
