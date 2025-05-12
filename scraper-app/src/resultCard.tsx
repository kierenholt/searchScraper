import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import { Result } from "./types";

interface ResultCardProps {
    result: Result;
}


export function ResultCard(props: ResultCardProps) {

    const css = {
        minWidth: "300px",
        width: "300px",
        margin: "0 20px",
        float: "left",
    };

    return (
        <Card sx={css} variant="outlined" color="primary">
            <CardContent>
                <Typography>
                    {props.result.place}th place:  {props.result.domain}
                </Typography>
            </CardContent>
        </Card>
    );
}