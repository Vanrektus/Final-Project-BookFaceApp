# BookFaceApp

<ins>**Application functionality:**</ins>

This is a facebook like application, with similar functionality ( not everything ) and definitely not even close look to the original app.

<ins>**Application access and more detailed functionality:**</ins>

<ins>Logged out user:</ins>
- **`Random publications`** - Home page that shows 3 random publications.
- **`Register`**
- **`Login`**

<ins>Logged in user - ***non admin***:</ins>
- **`Random Publications`** - Home page that shows 3 random publications.
- **`All publications`** - A page that shows all publications. This comes with a search bar, sorting and pagination functionality, like and comment functionality.
- **`Create a new publication`** - Publication creation page.
- **`All groups`** - A page that shows all groups. Similar to all publications, it comes with a search bar and sorting and pagination functionality. Users may request to join a group in order to see its publications.
- **`Create a new group`** - Group creation page. Publications that are added to groups ***must*** match the group's category!
- **`All users`** - A page where users can see all other users. Upon clicking on the name of an user, a page opens with all the specific user's publications.
- **`Current user's username ( profile page )'`** - A page with logged in user's publications.
- **`Like a publication`** - Users can like publications. If an user has already liked a publication, upon clicking it will unlike the publication.
- **`Comment a publication`** - Users can add comments to publications.
- **`Logout`**

<ins>Logged in user - ***admin***:</ins>
- Admins can do everything that non admins can do plus some more.
- **`Roles`** - Role managing page where admins can create/edit/delete roles and can manage users in those roles.
- **`Request`** - All user requests for joining a group show here. Admins can accept or deny them.

<ins>Editing and deleting access:</ins>
- ***Pleae note that <ins>ADMINS</ins> can edit and delete everything!***
- **`Publications`** - Users can only edit and delete their own publications.
- **`Comments`** - Comments can be edited and deleted by their creators, comment's publication's owner and if the comment's publication is in a group, the group owner can also edit and delete the comment.
- **`Groups`** - Groups can only be edited and deleted by their creators.