import Button from "@mui/material/Button";
import { useState } from "react";
import Grid from '@mui/material/Grid';
import { Result } from "./types";
import { Paper } from "@mui/material";

interface ResultsGridProps {
    results: string[];
    setSelectedResult: (i: number) => void;
    filter: string;
}


export function ResultsGrid(props: ResultsGridProps) {
    const [selectedIndex, setSelectedIndex] = useState<number>(-1);

    const gridCss = {
        minWidth: "300px",
        width: "300px",
        margin: "0 20px",
        float: "left"
    };

    const itemCss = {
        textAlign: "center",
        fontSize: "1rem",
        minWidth: "90%",
        padding: "1px 0px",
        margin: "0",
        height: "90%"
    };

    const clickHandler = (i: number) => {
        setSelectedIndex(i);
        props.setSelectedResult(i);
    }

    return (
        <Grid container sx={gridCss} spacing={0} columns={10}>
            {
                props.results.map((s: string,i: number) =>
                    <Grid size={1} key={i}>
                        <Button sx={itemCss} onClick={() => clickHandler(i)}
                            variant={i == selectedIndex ? "contained" : "outlined"}
                            color={props.filter && s.indexOf(props.filter) != -1 ? "primary" : "secondary"}>
                                {i + 1}
                        </Button>
                    </Grid>)
            }
        </Grid>
    )
}