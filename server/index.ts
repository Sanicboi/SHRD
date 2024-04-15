import express from 'express';

const app = express();

app.post('/api/user', async (req, res) => {
    res.status(200).end('token');
})

app.post('/api/login', async (req, res) => {
    res.status(200).end('token');
})


app.listen(80);