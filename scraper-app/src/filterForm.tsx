import { FormGroup, Stack, TextField } from "@mui/material";
import { ChangeEvent, useState } from "react";


interface FilterFormProps {
    setFilter: (s: string) => void;
}


export function FilterForm(props: FilterFormProps) {

    const onChangeHandler = (e: ChangeEvent<HTMLInputElement>) => {
        props.setFilter(e.target.value);
    }

    return (
        <FormGroup>
            <Stack direction={"row"} alignItems={"center"} justifyContent={"left"}>

                <TextField
                    id="filter"
                    label="filter"
                    helperText="filter results by this value"
                    placeholder="your company name"
                    variant="standard"
                    onChange={onChangeHandler}>
                </TextField>
            </Stack>
        </FormGroup>
    )
} 