import React, { useState, useEffect } from "react";
import { Grid, TextField, withStyles, FormControl, InputLabel, Select, MenuItem, Button, FormHelperText } from "@material-ui/core";
import useForm from "./validation";
import { connect } from "react-redux";
import * as actions from "../actions/CountryHistory";
import { useToasts } from "react-toast-notifications";

const styles = theme => ({
    root: {
        '& .MuiTextField-root': {
            margin: theme.spacing(1),
            minWidth: 230,
        }
    },
    formControl: {
        margin: theme.spacing(1),
        minWidth: 230,
    },
    smMargin: {
        margin: theme.spacing(1)
    }
})

const initialFieldValues = {
    Country: '',
    CountryCode: '',
    City: '',
    CityCode: '',
    Lat: '',
    Lon: '',
    Cases: '',
    Status: '',
    Date: '',

}

const CountryHistoryForm = ({ classes, ...props }) => {

    //toast msg.
    const { addToast } = useToasts()

  
    const validate = (fieldValues = values) => {
        let temp = { ...errors }
        if ('Country' in fieldValues)
            temp.Country = fieldValues.Country ? "" : "This field is required."
        if ('Cases' in fieldValues)
            temp.Cases = fieldValues.Cases ? "" : "This field is required."
        if ('Status' in fieldValues)
            temp.Cases = fieldValues.Status ? "" : "This field is required."
        setErrors({
            ...temp
        })

        if (fieldValues == values)
            return Object.values(temp).every(x => x == "")
    }

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    } = useForm(initialFieldValues, validate, props.setCurrentId)

   
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = React.useState(0);
    React.useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth);
    }, []);

    const handleSubmit = e => {
        e.preventDefault()
        if (validate()) {
            const onSuccess = () => {
                resetForm()
                addToast("Submitted successfully", { appearance: 'success' })
            }
            if (props.currentId == 0)
                props.createDCandidate(values, onSuccess)
            else
                props.updateDCandidate(props.currentId, values, onSuccess)
        }
    }

    useEffect(() => {
        if (props.currentId != 0) {
            setValues({
                ...props.dCandidateList.find(x => x.id == props.currentId)
            })
            setErrors({})
        }
    }, [props.currentId])

    return (
        <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
            <Grid container>
                <Grid item xs={12}>
                    <TextField
                        name="Cases"
                        variant="outlined"
                        label="Cases"
                        value={values.Cases}
                        onChange={handleInputChange}
                        {...(errors.Cases && { error: true, helperText: errors.Cases })}
                    />
                    <TextField
                        name="Status"
                        variant="outlined"
                        label="Status"
                        value={values.Status}
                        onChange={handleInputChange}
                        {...(errors.Status && { error: true, helperText: errors.Status })}
                    />
                    <FormControl variant="outlined"
                        className={classes.formControl}
                        {...(errors.Country && { error: true })}
                    >
                        <InputLabel ref={inputLabel}>Country</InputLabel>
                        <Select
                            name="Country"
                            value={values.Country}
                            onChange={handleInputChange}
                            labelWidth={labelWidth}
                        >
                            <MenuItem value="">Select Country</MenuItem>
                            <MenuItem value="AE">UAE</MenuItem>
                            <MenuItem value="In">India</MenuItem>
                            <MenuItem value="pk">Pakistan</MenuItem>
                           
                        </Select>
                        {errors.Country && <FormHelperText>{errors.Country}</FormHelperText>}
                    </FormControl>
               
                    <TextField
                        name="City"
                        variant="outlined"
                        label="City"
                        value={values.City}
                        onChange={handleInputChange}
                        {...(errors.City && { error: true, helperText: errors.City })}
                    />
                   
                    <TextField
                        name="Lat"
                        variant="outlined"
                        label="Lat"
                        value={values.Lat}
                        onChange={handleInputChange}
                    />
                    <TextField
                        name="Lon"
                        variant="outlined"
                        label="Lon"
                        value={values.Lon}
                        onChange={handleInputChange}
                    />
                    <div>
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            className={classes.smMargin}
                        >
                            Submit
                        </Button>
                        <Button
                            variant="contained"
                            className={classes.smMargin}
                            onClick={resetForm}
                        >
                            Reset
                        </Button>
                    </div>
                </Grid>
            </Grid>
        </form>
    );
}


const mapStateToProps = state => ({
    dCandidateList: state.dCandidate.list
})

const mapActionToProps = {
    createDCandidate: actions.create,
    updateDCandidate: actions.update
}


export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(CountryHistoryForm));