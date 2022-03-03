"use strict";

var username;
var socket = io();

do {
  username = prompt('Enter your name:');
} while (!username);

var textarea = document.querySelector('#textarea');
var submitBtn = document.querySelector('#submitBtn');
var commentBox = document.querySelector('.comment__box');
submitBtn.addEventListener('click', function (e) {
  e.preventDefault();
  var comment = textarea.value;

  if (!comment) {
    return;
  }

  postComment(comment);
});

function postComment(comment) {
  var data = {
    username: username,
    comment: comment
  };
  appendToDom(data);
  textarea.value = '';
  broadcastComment(data);
}

function appendToDom(data) {
  var ltag = document.createElement("li");
  ltag.classList.add('comment', 'mb-3');
  var markup = "\n                   <div class=\"card border-light mb-3\">\n                   <div class=\"card-body\"> \n                   <h3> ".concat(data.username, " : </h3>\n                   <p> ").concat(data.comment, "</p>\n                   <div >\n                       <small> ").concat(moment(data.time).format('LT'), "</small>\n                   </div>\n                   </div>\n                   </div>\n    ");
  ltag.innerHTML = markup;
  commentBox.prepend(ltag);
}

function broadcastComment(data) {
  socket.emit('comment', data);
}

socket.on('comment', function (data) {
  appendToDom(data);
});