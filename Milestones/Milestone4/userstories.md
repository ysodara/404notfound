# Peak Performance
Workout tracking application

This product is centered around four core features:
1. Workout creation for coaches that lets them assemble pieces of a workout from a database and assign it to athletes on their team. This includes details like how much weight, how many reps, and warmups and cooldown exercises. The athletes can then see a scheduled list of planned workouts, and start a workout to view details as they do the exercise.
1. A detailed profile page for athletes that shows their history of past workouts and schedule of upcoming workouts, and a link to start their next workout. This takes them to a separate page that lists each exercise of the workout in order. When completed there will be a window to leave notes about the workout which can then be viewed in the workout history.
1. An interpolation tool to help athletes set goals for improvement on max weight, distance, or speed with integration into the scheduler to help athletes make their goal into a concrete plan. This tool will work backwards from a goal, dividing it up into incremental improvements based on the athlete's current ability over a desired window of training. This will also be available for the coaches so they can plan how they want to push their athletes to improve.
1. Integration with Fitbit api for detailed biometric monitoring during workouts. Athletes will be able to authenticate their Fitbit device with the site which will then record data like heart rate, calories burned, and steps taken and add that data to the workout history. This is an optional add-on to the athlete profile and not an essential part of the user experience.

#### List of Needs and Features:
1. It is important for coaches to easily see the athletes on their team and be able to quickly put a workout together from exercises already stored in the database and assign it to their team. We would like to implement a feature that auto-populates workouts based on yearly records (We expect that the same workouts will be repeated for each training season) but this will require a year's worth of training information to already exist in the database.
1. Logins for both coaches and athletes. The coaches are admins and can add new athletes and workout items, and coaches have a team and can assign workouts to their team. A coach's profile shows their athletes' profiles and a list of upcoming workouts. The athlete's profile, as described above, shows their past and upcoming workouts. Athletes must first be added by their coaches. Coaches will add an athlete by email and the athlete can then use that email to create their account and link it to their name in the database.
1. Workouts are represented in our database as a list of workout items, each of which is a different exercise. Exercises have a type which is used for separating them into different categories, muscle group, and an associated record to keep track of individual athletes' performance.
1. There is no "Home page" for the site other than the front login page which only has buttons to sign up/login and some graphics advertising the functions of the app (A similar idea to Facebook's login page). Once logged in, the user's home page is their personal profile.
1. Initially this application is only meant for interoriganizational use. Since some coaches prefer to keep their athlete's workout schedules private from other teams the app is not designed for members of the public to view workout results. A later goal is to make the app available to public use in the coach-athlete context, where a group of friends for example could designate a coach to deploy workouts to other members of the team. In that context it would make more sense to make profiles public, and enable sharing of results to social media.

#### Non-Functional Requirements:
1. User data will be stored for as long as the associated user is an active part of a team. Once the user leaves there is no need to store their workout history anymore. Allow a possible grace period of a few weeks after departure before deletion and an option to export workout data if the user wants to save it for their own records.
1. The site should never return debug error pages, instead the web server should have a custom 404 page that looks nice and fits the overall visual theme and has a link to the user's main home page.
1. Some difficult exercises may include linked videos or diagrams to assist new athletes in proper form.

#### Functional Requirements (User Stories):
# Feature 1:
## Epic 1: As a coach, I would like a profile page where I can add athletes to my team.

### User Story 1:
#### Title:
As an athlete, I would like to be represented in the database by my name, team, gender, age, height, weight, email, and my own unique identity key so that my information can be stored securely and I can be easily identified in the application.

#### Assumptions/Preconditions:
The MVC project has been started using ASP.Net MVC 5 with .NET Framework 4.8 as a standard.

#### Description:
In order for new athletes to be added the above information must be included in their table to help plan workouts and gather information about them later. This information must be entered by the coaches. Models and views will be generated based on seed information entered in the database to give us the ability to create/edit/delete these entities.

#### Tasks:
1. Create a new database and server hosted on Azure as a part of our App resource group
1. Link database to our MVC project with the key hidden
1. Add an athlete table with a unique primary key, name, team, gender, age, height, weight, email
1. Seed the table with data
1. Generate models and views
1. Test by running the app and adding something to the table

#### Effort Points: 1

***

### User Story 2:
#### Title:
As a coach, I want a nice looking profile that allows me to add new athletes to the database by name with their gender, age, height, weight, and email so that I can keep track of who is on my team.
    
#### Assumptions/Preconditions:
The MVC project has been started and a database has been created to hold coaches and athletes. User accounts have been enabled, and the site has a home "Welcome" page with a navbar that has buttons to log in or sign up.
    
#### Description:
The coaches don't want their profile page cluttered with extra information. Access to athlete statistics will come later. For now, they just want to see a list of the athletes on their team, sorted alphabetically by last name, and to be able to create/edit/delete athletes in the database. In the next step, athletes will be added so they can create an account and log in by the email saved for them.

#### Tasks:
1. Create an admin area of the app
1. Create coach profile page, greeting them by name
1. Make coach profile the landing page after log in
1. Give coaches create/edit/delete permissions for athletes
1. Make the athlete name in the list a link to that athlete's index page (in place of their profile for now)

#### Effort Points: 1

***

## Epic 2: As a coach, I want to easily and quickly build a workout from items in the database so I can assign it to athletes on my team.
### User story 1:
#### Title:
As an athlete, I want a simple profile that shows my information (name, team, gender, age, height, weight, email) so I can see my team affiliation when I log in to the site.

#### Assumptions/Preconditions:
Logins have been built and athletes have been created in the database. This feature is built with the understanding that it will be of use once we have athletes using the site through their login. The profile is built first so we have a place for them to go after creating their account.

#### Description:
Coaches want their athletes to start with a simple, uncluttered layout for their profile that simply welcomes them to the site while we are building other features. At this point it is enough to just show their information and what team they are on. More features will be added in later user stories, and more data will be available once workouts have been completed by the athlete.

#### Tasks:
1. Create athlete user area of the app
1. Create athlete profile page that is part of the common layout and follows our design pattern
1. Add athlete information to the page

#### Effort Points: 1

***

### User story 2:
#### Title:
As a coach, I would like to be able to add athletes to my team by email so they can create accounts on the site and access their profile page.

#### Assumptions/Preconditions:
Login mechanism has been setup to allow coaches to create accounts. Athlete list page is addressed by the athlete ID in the database. Athlete profile is created

#### Description:
We will need a mechanism for sending notification emails to athletes with a link to the app so they can login using their email. This will give coaches the ability to securely limit who they allow to login and claim athlete accounts. The security of this step relies on the security of the athlete's own email. For our purposes at this point only a valid address within our organization is allowed, but in the future we may implement two factor authentication. Our team will research details of this tasks to learn how to implement it.

#### Tasks:
1. Research sending invitation emails hosted from the site
1. Attach a link to the site in the email
1. Limit the allowed addresses to emails inside our organization
1. Build menu for coaches to see which of their team has not been added yet
1. Create a button for coaches to press to send verification email to athletes

#### Effort points: 4

***

### User story 3:
#### Title:
As a coach, I would like to add exercises to the database, and then assemble those into workouts so I can then assign to athletes on my team.

#### Assumptions/Preconditions:
Adding athletes feature has been built and there are active athletes using the site. This feature will still work without that but there would be no one to see the workouts.

#### Description:
Coaches want an easy to use feature to add exercises to the database, and then assemble those exercises into a workout and assign that to their team. This will require adding more tables to our database and generating models and views for create/edit/delete. Coaches alone have permission to change workout information in the database. Acceptance criteria for this user story requires the feature to be fast, responsive, and easy to use.

#### Tasks:
1. Add a button on the coach's navbar to create new exercise item
1. Add a button on the coach's navbar to create new workout
1. Build a page for creating/editing/deleting exercise items from the database
1. Build a new page that displays choices of exercise items to create a workout
1. Add to workout creation page a selection of which athletes to assign it

#### Effort Points: 2

***

# Feature 2:
Epic: As an athlete I would like to have a profile page that shows my upcoming workouts.

Epic: As an athlete I want to have a workout page that shows the individual exercises of the workout I started.

Epic: As an athlete I would like to see my past workouts on my profile.

Epic: As an athlete I would like to have a place to leave notes at the end of my workout that will be saved to my workout history.

# Feature 3:

Epic: As an athlete, I would like to have a tool that helps me set a workout goal and see incremental steps to reach that goal.

Epic: As a coach, I would like to use a tool to plan how my athletes can improve to meet a goal I have set for them.

# Feature 4:

Epic: As an athlete, I would like to be able to link my Fitbit device to the app so I can track biometric data along with my workouts for more enhanced history about my workouts.