# simple-csv-importer

### Requirements
- Docker

### Execution guide
To start the application navigate to repo root folder and run ```docker compose up```

It may take some time to startup because database container needs to start first.
Api container may restart multiple times in the proccess.

After both containers are up and running navigate to ```http://localhost:5000/swagger/index.html```

You can use ```samples/sample.csv``` in repo folder to import test data.
