const express = require('express')
const app = express()
const bodyParser = require('body-parser')
const MongoClient = require('mongodb').MongoClient

var db

MongoClient.connect('mongodb://icristian10:icristian10@ds129462.mlab.com:29462/opiniondb', (err, database) => {
  if (err) return console.log(err)
  db = database
  app.listen(process.env.PORT || 3000, () => {
    console.log('listening on 3000')
  })
})

app.set('view engine', 'ejs')
app.use(bodyParser.urlencoded({extended: true}))
app.use(bodyParser.json())
app.use(express.static('public'))

app.get('/', (req, res) => {
  db.collection('opinions').find().toArray((err, result) => {
    if (err) return console.log(err)
    res.render('index.ejs', {opinions: result})
  })
})

app.post('/opinions', (req, res) => {
  db.collection('opinions').save(req.body, (err, result) => {
    if (err) return console.log(err)
    console.log('saved to database')
    res.redirect('/')
  })
})

app.put('/opinions', (req, res) => {
  db.collection('opinions')
  .findOneAndUpdate({name: 'James Bond'}, {
    $set: {
      name: req.body.name,
      opinion: req.body.quote
    }
  }, {
    sort: {_id: -1},
    upsert: true
  }, (err, result) => {
    if (err) return res.send(err)
    res.send(result)
  })
})

app.delete('/opinions', (req, res) => {
  db.collection('opinions').findOneAndDelete({name: req.body.name}, (err, result) => {
    if (err) return res.send(500, err)
    res.send('These were the last words from James')
  })
})
