# Blog BackEnd API - Project Overview

## Project Goal

Create a backend for Blog Application:

- Support full CRUD operations
- All users to create account and login
- Deploy to Azure
- Uses SCRUM workflow concepts
-Introduces Azure DevOps practices

## Stack

    - Back end will be in .Net 9, ASP.NET Core, EF Core, SQL server.
    - Front End will be done in NEXT JS with TypeScript (To be done by. Jacob) flowbite(tailwind). Deploy with TBA (Vercel or Azure)

    ## Application Features

    ### User Capabilities

    - Create and account
    - Login
    - Delete account 

### Blog Features

    - View all published blog posts
    - Filter blog posts
    - Create new posts 
    - Edit existing posts
    - Delete posts
    - Publish or Unpublish posts

### Pages (Frontend Connected to API)

- Create Account Page
- Blog view post page of published items 
- Dashboard page (this is the profile page will edit, delete, and publish and unpublish our blog posts)

- **Blog Page**
    - Display all published blog items

- **Dashboard**
    - User profile page
    - Create blog posts
    - Edit blog posts
    - Delete blog posts

## Project Folder Structure

### Controllers

#### UserController

Handles all user interaction. 

Endpoints:

- Login 
- Add user
- Update user
- Delete user

#### BlogController

Handles all blog operations.

Endpoints:

- Create Blog Item (C)
- Get All Blog Items (R)
- Get Blog Items By Category (R)
- Get Blog Items By Tags (R)
- Get Blog Items By Date (R)
- Get Published Blog Items (R)
- Update Blog Items (U)
- Delete Blog Items (D)
- Get Blog Items By UserId

> Delete will be used for soft delete / publish logic

---

## Models

### UserModel

```csharp

int id
string Username
string Salt
string Hash

### BlogItemModel

int id
int UserId
string PublisherName
string Title
string Image
string Description
string Date
string Category
string Tags
bool IsPublished
bool IsDeleted

## Items Saved to your DB
### We need a way to sign in with our user name and password

```csharp

### LoginModel

    string Username
    string Password

### CreatedAccountModel
    int Id = 0
    string Username
    string Password

### PasswordModel

    string Salt
    string Hash

```

### Services
    Context/Folder
    - DataContext
    -UserServices/file
        -GetUserByUsername
        -Login
        -AddUser
        -UpdateUser
        -DeleteUser
### BlogItemService
    - AddBlogitems
    - GetAllBlogItems
    - GetBlogItemsByCategory
    - GetBlogItemsByTags
    - GetBlogItemsByDate
    - GetPublishedBlogItems
    - UpdateBlogItems
    - DeleteBlogItems
    - GetUserById
### PasswordServices

    - Hash Password
    - Verify Hash Password