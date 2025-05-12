
# build
    git clone https://github.com/kierenholt/searchScraper.git searchScraperTest
    cd searchScraperTest
    cd scraper-app
    npm install
    cd ..
    
# run the api
    cd scraper-api
for mocked search results
    dotnet run --launch-profile "mocked"
for actual search results (fetched using puppeteer)
    dotnet run --launch-profile "http"

# run the app
    cd scraper-app
    npm run start
