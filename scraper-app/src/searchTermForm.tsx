import { Button, FormGroup, Stack, TextField } from "@mui/material";
import { ChangeEvent, useState } from "react";
import { Helpers } from "./helpers";


interface SearchTermFormProps {
    setSearchResults: (s: string[]) => void;
}


export function SearchTermForm(props: SearchTermFormProps) {
    const [searchTerm, setSearchTerm] = useState<string>("");
    const [disabled, setDisabled] = useState<boolean>(false);

    const onChangeHandler = (e: ChangeEvent<HTMLInputElement>) => {
        setSearchTerm(e.target.value);
    }

    const clickHandler = () => {
        let preparedSearchTerm = searchTerm.replace(" ","+"); 
        setDisabled(true);
        Helpers.get<string[]>(`/api/search/${preparedSearchTerm}`)
            .then((results: string[]) => {
                if (results) {
                    console.log(results);
                    props.setSearchResults(results);
                }
                else {
                    throw("error fetching results")
                }
                setDisabled(false);
            })
    }

    return (
        <FormGroup>
            <Stack direction={"row"} alignItems={"center"} justifyContent={"space-between"}>

                <TextField
                    id="search-term"
                    label="search terms"
                    helperText="search terms to enter into google"
                    placeholder="land registry search"
                    variant="standard"
                    value={searchTerm}
                    onChange={onChangeHandler}
                    sx={{width: "40%"}}>
                </TextField>
                <Button variant="contained" sx={{ padding: "0.5rem", width: "40%" }}
                    disabled={searchTerm.length == 0 || disabled}
                    onClick={clickHandler}>
                    Get search results
                </Button>
            </Stack>
        </FormGroup>
    )
} 