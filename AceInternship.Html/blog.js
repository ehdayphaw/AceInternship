const tblBlog = "blogs";
let blogId = null;
getBlogTable();

//createBlog();
//updatBlog("c08155b1-ca33-42fa-80d0-42c595c99a77","Love","You","So match");
//deleteBlog("c08155b1-ca33-42fa-80d0-42c595c99a77");

function readBlog() {
    let lst = getBlogs();
    console.log(lst);
}


function createBlog(title, author, content) {
   

    let lst = getBlogs();
    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);
   
    clearControls();
}

function editBlog(id) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length);

    if (items.length == 0) {
        console.log("no data found.");
        errorMessage("No data Found.");
        return;
    }
    //return items[0];
    let item = items[0];

    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();
}
function updatBlog(id, title, author, content) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length);

    if (items.length == 0) {
        console.log("no data found.");
        errorMessage("No data Found.");
        return;
    }
    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;
    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);
    
}

function deleteBlog(id) {
    Swal.fire({
        title: "Are you sure to delete?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            let lst = getBlogs();
            const items = lst.filter(x => x.id === id);

            if (items.length == 0) {
                console.log("no data found.");
                errorMessage("No data found.");
                return;
            }

            lst = lst.filter(x => x.id !== id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);
            successMessage("Deleted successfully.");
            getBlogTable();
        }
    });
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}
function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];      //list not use error happen       
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }

    return lst;

}
$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if (!title || !author || !content) {
        errorMessage("All fields are required.");
        return;
    }

    Notiflix.Loading.standard();

    setTimeout(() => {
        Notiflix.Loading.remove();
        if (blogId === null) {
            createBlog(title, author, content);
            successMessage("Saved successfully.");
        } else {
            updatBlog(blogId, title, author, content);
            blogId = null;
            successMessage("Updated successfully.");
        }

        clearControls();
        getBlogTable();
    }, 2000);
});

function clearControls() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')" >Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
        `;   
        htmlRows += htmlRow;
    });
    $('#tbody').html(htmlRows);
}

function successMessage(message){
    Swal.fire({
    title: "Success!",
    text: message,
    icon: "success"
});
}
function errorMessage(message){
    Swal.fire({
    title: "Error!",
    text: message,
    icon: "error"
});
}



//completely