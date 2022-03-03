
const { response } = require('express')
const express = require('express')

const app = express()

const port = process.env.PORT || 3000

app.use(express.static('public'))

const dbConnect = require('./db')
dbConnect()

const Comment = require('./Models/comments')
app.use(express.json())

app.post('/api/comments', (req, res) => {

    const comment = new Comment({
        username: req.body.username,
        comment: req.body.comment
    })

    comment.save().then(response=>{
        res.send(response)
    })
    
});



app.get('/api/comments', (req, res) => {
    
    Comment.find().then(function(comments){
        res.send(comments)
    })
    
});

const server = app.listen(port, () => {
    console.log(`Server started on port ${port}`)
})
 
let io = require('socket.io')(server)

io.on('connection',(socket) => {

    console.log(`New Connection ${socket.id}`)

    socket.on('comment',(data) => {
        data.time = Date()
        socket.broadcast.emit('comment',data)
    })
})
