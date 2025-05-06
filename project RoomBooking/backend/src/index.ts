import express from 'express';

const app = express();
const port = process.env.PORT || 3001;

app.get('/', (_req, res) => {
  res.send('Backend is working!');
});

app.listen(port, () => {
  console.log(`Backend listening on port ${port}`);
});
