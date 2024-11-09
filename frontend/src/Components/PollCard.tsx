'use client'

import React, { useState } from 'react';
import { Box, Card, CardContent, Typography, Button, TextField, Snackbar, Alert, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';
import { pollService } from '../Services/api';
import { Poll } from '../Services/Types';

interface PollCardProps {
    poll: Poll;
    onVoteSubmitted: () => void;
}

const PollCard: React.FC<PollCardProps> = ({ poll, onVoteSubmitted }) => {
    const [selectedOption, setSelectedOption] = useState<string | null>(null);
    const [email, setEmail] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [showSuccess, setShowSuccess] = useState(false);
    const [openDialog, setOpenDialog] = useState(false);

    const handleOpenDialog = () => {
        setOpenDialog(true);
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
        setSelectedOption(null);
        setEmail('');
    };

    const handleVote = async () => {
        if (!selectedOption || !email) {
            setError('Please select an option and enter your email');
            return;
        }

        try {
            console.log("Sending vote with pollId:", poll.id, "and optionId:", selectedOption);
            await pollService.vote(poll.id, selectedOption, email);
            setShowSuccess(true);
            handleCloseDialog();
            onVoteSubmitted();
        } catch (err: any) {
            console.error('Vote error:', err);
            setError(err.message || 'Error submitting vote');
        }
    };

    return (
        <div className="w-full mb-4">
            <Card sx={{
                border: 1,
                borderColor: '#000000',
                borderRadius: 2,
                boxShadow: 3,
                width: 1
            }}>
                <CardContent>
                    <Typography variant="h5" gutterBottom>
                        {poll.name}
                    </Typography>

                    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
                        {poll.options && poll.options.map((option) => (
                            <Box key={option.id} sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                                <Typography>{option.name}</Typography>
                                <Typography>{option.votes} votes</Typography>
                            </Box>
                        ))}

                        <Button variant="contained" onClick={handleOpenDialog} fullWidth>
                            Vote
                        </Button>
                    </Box>
                </CardContent>
            </Card>

            <Dialog open={openDialog} onClose={handleCloseDialog}>
                <DialogTitle>Vote en {poll.name}</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Select an option and provide your email to submit your vote.
                    </DialogContentText>

                    {poll.options && poll.options.map((option) => (
                        <Button
                            key={option.id}
                            variant={selectedOption === option.id ? "contained" : "outlined"}
                            onClick={() => setSelectedOption(option.id)}
                            fullWidth
                            sx={{ my: 1 }}
                        >
                            {option.name}
                        </Button>
                    ))}

                    <TextField
                        label="Your Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        fullWidth
                        margin="normal"
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseDialog} color="secondary">
                        Cancel
                    </Button>
                    <Button onClick={handleVote} color="primary" disabled={!selectedOption || !email}>
                        Send Vote
                    </Button>
                </DialogActions>
            </Dialog>

            <Snackbar open={!!error} autoHideDuration={6000} onClose={() => setError(null)}>
                <Alert severity="error" onClose={() => setError(null)}>
                    {error}
                </Alert>
            </Snackbar>

            <Snackbar open={showSuccess} autoHideDuration={6000} onClose={() => setShowSuccess(false)}>
                <Alert severity="success" onClose={() => setShowSuccess(false)}>
                    ¡Successfull!
                </Alert>
            </Snackbar>
        </div>
    );
};

export default PollCard;
