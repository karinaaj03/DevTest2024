import React from 'react';
import styles from '../Styles/PollCard.module.css'
import {Box, Card, CardContent, Typography} from '@mui/material';

interface PollOptions {
    name: string;
    votes: number;
}
interface Poll {
    name: string;
    options: PollOptions[];
}

const PollCard = ({poll} : { poll: Poll }) => {
    return (
        <div className="w-full">
            <Card sx={{
                border: 1,
                borderColor: '#000000',
                borderRadius: 2,
                boxShadow: 3,
                width: 1,
                transition: 'transform 0.3s',
                '&:hover': {
                    transform: 'scale(1.05)'
                }
            }}>
                <CardContent>
                    <Box>
                        <Typography variant="h5" gutterBottom>
                            {poll.name}
                        </Typography>
                        <Typography sx ={{textDecoration: 'underline'}} variant="h5" gutterBottom>
                            Vote
                        </Typography>
                    </Box>
                    <Box sx={{ display: 'flex', gap: 2 }}>
                        <Typography variant="h5" gutterBottom>
                            {poll.options.map(option => (
                                option.name
                            ))}
                        </Typography>
                        <Typography className={styles.votes} variant="h5" gutterBottom>
                            {poll.options.map(option => (
                                option.votes
                            ))}
                        </Typography>
                    </Box>
                </CardContent>
            </Card>
        </div>
    );
};

export default PollCard;