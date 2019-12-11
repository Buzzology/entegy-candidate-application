import React, { useState } from 'react';
import './App.css';
import ChequeDisplay from './components/cheques/ChequeDisplay';
import { Grid, Container } from '@material-ui/core';
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles';
import { blue, pink } from '@material-ui/core/colors';
import CssBaseline from '@material-ui/core/CssBaseline'
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import ChequeCreateDialog from './components/cheques/ChequeCreateDialog';

const theme = createMuiTheme({
  palette: {
    primary: blue,
    secondary: pink,
  },
  typography: {
    useNextVariants: true,
  },
});

function App() {

  const [showCreateDialog, setShowCreateDialog] = useState(false);
  const [cheques, setCheques] = useState([
    { name: "Chris Owens", amount: 49.95, amountInWords: "FORTY-NINE DOLLARS AND NINETY-FIVE CENTS", createdDate: new Date() }
  ]);

  function addCheque(cheque) {
    setCheques([...cheques, cheque]);
    setShowCreateDialog(false);
  }

  return ([
    <MuiThemeProvider theme={theme} key="app">
      <Container maxWidth="md" style={{ marginTop: 40 }}>
        <Grid container spacing={3}>
          <Grid item xs={12} style={{ textAlign: 'right' }}>
            <Fab color="primary" aria-label="add" onClick={() => setShowCreateDialog(true)}>
              <AddIcon />
            </Fab>
          </Grid>
          {cheques.map(cheque => (
            <Grid item xs={12} key={cheque.createdDate.toTimeString()} style={{ marginBottom: '24px' }}>
              <ChequeDisplay
                name={cheque.name}
                amount={cheque.amount}
                amountInWords={cheque.amountInWords}
                createdDate={cheque.createdDate}
              />
            </Grid>
          ))}

          <ChequeCreateDialog visible={showCreateDialog} closeCreateFunc={() => setShowCreateDialog(false)} onCreateSuccess={addCheque} />
        </Grid>
      </Container>
    </MuiThemeProvider>,
    <CssBaseline key="css-baseline" />
  ]);
}

export default App;
