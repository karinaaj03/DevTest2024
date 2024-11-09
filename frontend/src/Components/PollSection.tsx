'use client'

import React, {useState, useEffect} from 'react';
import styles from '../Styles/PollSections.module.css'
import { Box, Container, Grid, TextField, Button } from "@mui/material";
import {pollService} from '../Services/api'
import PollCard from "@/Components/PollCard";

const PollSection = () => {
    const [dataPolls, setDataPolls] = useState([]);

    useEffect(() => {
        const polls = async () => {
            const bookData = await pollService.getPolls();
            setDataPolls(bookData);
        };
        polls();
    }, []);

    return (
        <div className="w-full">
            <Box sx={{py : 6}}>
                <Container maxWidth="xl">
                    {dataPolls.map((poll: any) => (
                        <Grid key={poll.name}>
                            <PollCard poll={poll}/>
                        </Grid>
                    ))}
                </Container>
            </Box>
        </div>
    );
};

export default PollSection;