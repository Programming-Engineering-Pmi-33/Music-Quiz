
#!/bin/bash

CURRENT_DIRECTORY=$(dirname "$0")
cd $CURRENT_DIRECTORY

echo "Running database updater."
dotnet ef database update