# The World App
A simple world travelling app developed by following [Shawn Wildermuth's Pluralsight Tutorial](https://app.pluralsight.com/library/courses/aspdotnet-5-ef7-bootstrap-angular-web-app/table-of-contents)

### Dev Commands

View all available runtimes
```
dir c:\users\[username]\.dnx\runtimes
```

Updating Bower packages
```
bower update
```

Building the application
```
dnx web
```

Adding Asp Identities to the database migration script
```
dnx ef migrations add IdentityEntities
```

Undoing action
```
ef migrations remove
```

Node Packages
```
npm install will install both "dependencies" and "devDependencies"

npm install --production will only install "dependencies"

npm install --dev will only install "devDependencies"
```

### Deployment Commands

Minify JS
```
gulp minify
```

Publishing
```
dnu publish -o [destination folder]
or
dnu publish -o [destination folder] --runtime [runtime name]
```