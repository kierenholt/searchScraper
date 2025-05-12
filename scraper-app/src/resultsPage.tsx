import { useState } from "react";
import { SearchTermForm } from "./searchTermForm";
import { ResultsGrid } from "./resultsGrid";
import { Result } from "./types";
import { ResultCard } from "./resultCard";
import { FilterForm } from "./filterForm";


export function ResultsPage() {
    const [results, setResults] = useState<string[]>([]);
    const [selectedResult, setSelectedResult] = useState<Result | null>(null);
    const [filter, setFilter] = useState<string>("");

    const setResultFromIndex = (i: number) => {
        if (i > 0 && results[i]) {
            setSelectedResult({
                domain: results[i], place: i + 1
            });
        }
        else {
            setSelectedResult(null);
        }
    }

    return (
        <>
            <SearchTermForm setSearchResults={(s: string[]) => setResults(s)} />
            <FilterForm setFilter={(s) => setFilter(s)} />

            <ResultsGrid results={results}
                setSelectedResult={(i: number) => setResultFromIndex(i)}
                filter={filter} />
            {
                selectedResult && <ResultCard result={selectedResult} />
            }
        </>

    );
}