import { createTheme, ThemeProvider } from '@mui/material';
import './App.css';
import { ResultsPage } from './resultsPage';
import { blue, grey } from '@mui/material/colors';

function App() {

	const theme = createTheme({
		palette: {
			primary: blue,
			secondary: grey,
		},
	});

	return (
		
		<div style={{margin: "10px"}}>
			<ThemeProvider theme={theme}>
				<h1>Search scraper</h1>
				<ResultsPage />
			</ThemeProvider>
        </div>
	);
}

export default App;
