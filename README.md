# Blog Managment Backend Project
this is a API Backend project i made for the sake of learning more anout backend 
it's my first 'big' project
i look forward to learn more and more 
        		   
# Details
this simple api has 5 models in total which are : Users , Posts , Comments , likes and tags 
Users can be Admins : 0 , Editors : 1 , Readers : 2  from the Role property
it has the simple CRUD operations 
it has Authentication and Authorization using JWT tokens with JWTBearer
you can 'POST' a user in the user controller then Authenticate it in the Auth controller 
you can reject/approve comments (they're 'pending' by default) using the comments controller (Admin only)
you can show only approved comments in a post using the post controller 
you can filter/serach through Users , Posts and Comments each in their own Controller 
