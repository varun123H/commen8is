"use strict";

var express = require('express');

var app = express();
var port = process.env.PORT || 3000;
app.use(express["static"]('public'));
var server = app.listen(port, function () {
  console.log("Server started on port ".concat(port));
});

var io = require('socket.io')(server);

io.on('connection', function (socket) {
  console.log("New Connection ".concat(socket.id));
  socket.on('comment', function (data) {
    data.time = Date();
    socket.broadcast.emit('comment', data);
  });
});