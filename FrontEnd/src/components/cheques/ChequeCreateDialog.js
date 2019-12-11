import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { FormControl } from "@material-ui/core";

const intialChequeValues = { name: '', amount: 0 };

function ChequeCreateDialog({ visible, closeCreateFunc, onCreateSuccess }) {

    const [loading, setLoading] = useState(false);
    const [cheque, setCheque] = useState(intialChequeValues);
    const internalChangeHandler = e => HandleChange(e, cheque, setCheque);

    return (
        <Dialog
            open={visible}
            onClose={closeCreateFunc}
            aria-labelledby="form-dialog-title"
        >
            <DialogTitle id="form-dialog-title">Create Cheque</DialogTitle>
            <DialogContent>

                <DialogContentText>
                    Create a new cheque by providing a name and amount.
                </DialogContentText>

                <FormControl fullWidth margin="normal">
                    <TextField
                        required
                        autoFocus
                        label="Payee Name"
                        name="name"
                        type="text"
                        placeholder="John Doe"
                        onChange={internalChangeHandler}
                    />
                </FormControl>

                <FormControl fullWidth margin="normal">
                    <TextField
                        required
                        autoFocus
                        label="Amount"
                        name="amount"
                        type="number"
                        placeholder="49.95"
                        onChange={internalChangeHandler}
                    />
                </FormControl>

            </DialogContent>
            <DialogActions>
                <Button onClick={closeCreateFunc} color="default" variant="text">
                    Cancel
                </Button>
                <Button onClick={() => createCheque(cheque, onCreateSuccess, setLoading, setCheque)} color="primary" variant="contained" disabled={loading}>
                    {loading ? "Loading..." : "Create"}
                </Button>
            </DialogActions>
        </Dialog >
    )
}


/* Handles input forms for a component change event */
const HandleChange = (event, stateObject, setter) => {

    const target = event.target;
    const value = GetHandleChangeValue(event);
    const name = target.name;

    if (!name) {
        console.log("A name attribute is required.");
        return;
    }

    stateObject[name] = value;
    setter({ ...stateObject });
}


/* Used to retrieve the value of a changed input field */
const GetHandleChangeValue = (event) => {
    const target = event.target;
    return target.type === 'checkbox' ? target.checked : target.value;
}


/* Create a new cheque */
async function createCheque(cheque, onCreateSuccess, setLoading, setCheque) {

    setLoading(true);

    try {
        var resp = await fetch(`https://localhost:44394/cheques/amountInWords?amount=${cheque.amount}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        });

        if(resp.status !== 200){
            alert(resp.statusText);
        }

        var json = await resp.json();
        if(json && json.data){

            // Add the amount in words to the cheque object and return it
            if(json.data.amountInWords){
                setCheque(intialChequeValues);
                return onCreateSuccess({
                    ...cheque,
                    amountInWords: json.data.amountInWords,
                    createdDate: new Date(),
                });
            }

            // Check if any error messages were returned
            if(json.messages && json.messages.length){
                for(var message of json.messages){
                    alert(message.text);
                }

                return;
            }
        }
        
        alert("Failed to retrieve amount in words.");
    }
    catch (e) {
        console.error(e);
    }
    finally {
        setLoading(false);
    }
}


export default ChequeCreateDialog;