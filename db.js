function dbConnect(){
    const mongoose = require('mongoose')
    const url = process.env.MONGO_URL || 'mongodb://localhost:27017/comments'

     mongoose.connect(url, {
         useNewUrlParser : true,
         useUnifiedTopology:true,
         useFindAndModify:true
     }) 
     
     const connection = mongoose.connection
     connection.once('open', function(){
         console.log('Database connected..')
     }).catch(function(err){

        console.log('connection failed...')

     })
}

module.exports= dbConnect