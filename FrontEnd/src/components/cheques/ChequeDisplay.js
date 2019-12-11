import React from 'react';
import WestpacLogo from '../../assets/img/westpac-vector-logo.svg'
import Signature from '../../assets/img/signature.png'
import '../../assets/css/index.css'
import { Grid, Typography } from '@material-ui/core';
import moment from 'moment';

function ChequeDisplay({ name, amount, amountInWords, createdDate }) {

    return (
        <Grid container spacing={3} className="cheque-wrapper">
            <Grid item xs={3} className="cheque-logo-wrapper">
                <img src={WestpacLogo} alt="Westpac Logo" className="img-logo" /><br />
            </Grid>
            <Grid item xs={6}>
                <Typography noWrap>Westpac Banking Corporation</Typography>
            </Grid>
            <Grid item xs={3}>
                <div className="cheque-date-wrapper">
                    <Typography noWrap>{moment(createdDate).format('DD/MM/YYYY')}</Typography>
                </div>
            </Grid>
            <Grid item xs={12}>
            <Typography noWrap>FORTITUDE VALLEY Q</Typography>
            </Grid>
            <Grid item xs={7}>
                <div className="cheque-pay-wrapper">
                    <Typography noWrap>
                        <small>Pay</small> {name}&nbsp;
                        <small>or bearer</small>
                    </Typography>
                </div>
            </Grid>
            <Grid item xs={5}>
                <div className="cheque-amount-wrapper">
                    <Typography noWrap>$ <span className="cheque-amount-number">{Number(amount).toFixed(2)}</span></Typography>
                </div>
            </Grid>
            <Grid item xs={12}>
                <div className="cheque-amount-words-wrapper">
                    <Typography noWrap><small>The sum of</small> {amountInWords}</Typography>
                </div>
                <div className="cheque-from-wrapper">
                    ENTEGY PTY LTD
                </div>
            </Grid>
            <Grid item xs={12} style={{ textAlign: 'right', position: 'relative' }}>
                <img src={Signature} alt="Entegy Signature" className="cheque-signature" />
            </Grid>
            <Grid item xs={12} className="font-consolas cheque-code-wrapper">
                <Typography noWrap>" 204565 "055"' 123-: 98"'1974"-</Typography>
            </Grid>
            <div className="cheque-not-negotiable">
                NOT NEGOTIABLE<br />
                <small>CREDIT BANK ACCOUNT PAYEE ONLY</small>
            </div>
        </Grid>
    )
}

export default ChequeDisplay;