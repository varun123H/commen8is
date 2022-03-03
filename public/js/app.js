


let username

let socket = io()




do {
    username = prompt('Enter your name:')
}while(!username)

const textarea = document.querySelector('#textarea')
const submitBtn = document.querySelector('#submitBtn')
const commentBox = document.querySelector('.comment__box')

submitBtn.addEventListener('click',(e) => {
    e.preventDefault()
    let comment = textarea.value 
    if(!comment){
        return
    }

    postComment(comment)
})

function postComment(comment){
    let data = {
        username : username,
        comment : comment
    }

    appendToDom(data)
    textarea.value = ''

    broadcastComment(data)

    syncWithDb(data)
}

function appendToDom(data){
    let ltag = document.createElement("li")
    ltag.classList.add('comment', 'mb-3')

    let markup = `
                   <div class="card border-light mb-3">
                   <div class="card-body"> 
                   <h3> ${data.username} : </h3>
                   <p> ${data.comment}</p>
                   <div >
                       <small> ${moment(data.time).format('LT')}</small>
                   </div>
                   </div>
                   </div>
    `
    ltag.innerHTML = markup
    commentBox.prepend(ltag)
}

function  broadcastComment(data){

     socket.emit('comment', data)

}

socket.on('comment',(data) => {

    appendToDom(data)
})

function  syncWithDb(data){
    const headers = {
        'Content-Type' : 'application/json'
    }

    fetch('/api/comments', {method: 'Post', body:JSON.stringify(data), headers})
                 .then(response => response.json())
                 .then(result => {
                     console.log(result)
                 })
}

function fetchComments(){
    fetch('/api/comments')
    .then(res => res.json())
    .then(result => {
             result.forEach((comment) =>{

                comment.time = comment.createdAt
                appendToDom(comment)

             })


            appendToDom()
            console.log(result)
        })
    
}
 window.onload = fetchComments