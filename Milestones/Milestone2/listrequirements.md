## Identify Non-Functional Requirements

1. User accounts and data must be stored indefinitely.  They don't want to delete; rather, mark items as "deleted" but don't actually delete them.  They also used the word "inactive" as a synonym for deleted.
2. Passwords should not expire
3. Site should never return debug error pages.  Web server should have a custom 404 page that is cute or funny and has a link to the main index page.
4. All server errors must be logged so we can investigate what is going on in a page accessible only to Admins.
5. English will be the default language.
6. Each email address used to register for an account my be verified and unique.
7. Webpage must not include specific team colors, rather a classy greyscale color scheme
8. 

## Identify Functional Requirements (User Stories)

E: Epic  
U: User Story  
T: Task  

1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.
   1. [T] Create starter ASP dot NET MVC 5 Web Application with Individual User Accounts and no unit test project
   2. [T] Choose CSS library (Bootstrap 3, 4, or ?) and use it for all pages
   3. [T] Create nice homepage: write initial content, customize navbar, hide links to login/register, clean modern greyscale color scheme
   4. [T] Create SQL Server database on Azure and configure web app to use it. Hide credentials.
2. [U] As a visitor to the site I would like to be able to register an account so I will be able to access athlete statistics
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP script
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
   4. [T] Re-enable login/register links
   5. [T] Manually test register and login; user should easily be able to see that they are logged in
3. [E] As an administrator I want to be able to upload a spreadsheet of results so that new data can be added to our system
    1. [T] Create template for spreadsheet to be uploaded
    2. [T] Write parser for spreadsheet template and SQL for upload to database
    3. [T] Ensure real-time update and display of new data
4. [U] As a visitor I want to be able to search for an athlete and then view their athlete page so I can find out more information about them
    1. [T] Create search bar and search functionality
    2. [T] Allow for a visitor to search by name or by team.
    3. [T] Create athlete "profile" pages for each athlete, redirect to athlete page on click from search
5. [U] As a visitor I want to be able to view race results for an athlete so I can see how they have performed
    1. [T] Display race results chronologically for all races competed in on athlete page
6. [U] As a visitor I want to be able to view PR's (personal records) for an athlete so I can see their best performances
    1. [T] Display PR for each event tht eathlete has competed in on athlete page
7. [E] As a coach I want to be able to view all athletes, so I can form up my team.
    1. [U] As a coach, I want to be able tpo specify the athletes on my team so that I can use analytical software to predict their performance and build my meet strategy.
8. [E] As a coach I want to be able to view the stats for an athlete, so I can organize my team for best performance.
    1. [U] As a coach I want to be able to show PR's in each race event, so I know the best event for that athlete.
    2. [U] As a coach I want to be able to see past performance for an athlete on a picture/plot based on race type and distance, so I can track their performance.
    3. [U] As a coach I want to be able to show all athletes in ascending order based on their performance, so I can see who is the best athlete for each event.
9. [U] As a robot I would like to be prevented from creating an account on your website so I don't ask millions of my friends to join your website and try to add comments about male enhancement drugs.
    1. [T] Utilize human verification upon registration for account.
10. [E] As a coach I want to be able to track athlete interests in race event, so I can assign them based on race event.
    1. [U] As coach I want to be able to show how often an athlete compete in each race event, so I can know which event they are comfortable in, and which one they need to be improved.
11. [E] As an athlete, I want to post my race results on social media so that I can share a link to my athlete profile with my friends and family.

