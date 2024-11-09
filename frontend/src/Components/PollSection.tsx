'use client'

import React, { useState, useEffect, useCallback } from 'react';
import { Box, Container, Grid } from "@mui/material";
import { pollService } from '../Services/api';
import PollCard from "@/Components/PollCard";
import { Poll } from '../Services/Types';

const PollSection = () => {
    const [dataPolls, setDataPolls] = useState<Poll[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchPolls = useCallback(async () => {
        try {
            const polls = await pollService.getPolls();
            console.log('Fetched polls:', polls); // Para debug
            setDataPolls(polls);
        } catch (err) {
            console.error('Error fetching polls:', err);
            setError('Failed to load polls');
        }
    }, []);

    useEffect(() => {
        fetchPolls();
    }, [fetchPolls]);

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div className="w-full">
            <Box sx={{py: 6}}>
                <Container maxWidth="xl">
                    <Grid container spacing={3}>
                        {dataPolls && dataPolls.length > 0 ? (
                            dataPolls.map((poll) => (
                                <Grid item xs={12} key={poll.id}>
                                    <PollCard
                                        poll={poll}
                                        onVoteSubmitted={fetchPolls}
                                    />
                                </Grid>
                            ))
                        ) : (
                            <Grid item xs={12}>
                                <div>No polls available</div>
                            </Grid>
                        )}
                    </Grid>
                </Container>
            </Box>
        </div>
    );
};

export default PollSection;