import axios from 'axios';

export const pollService = {
    getPolls : async () => {
        try {
            const response = await axios.get('http://localhost:5248/api/v1/Poll');
            return response.data.map((poll) => ({
                id: poll.id,
                name: poll.name,
                options: poll.options.map((option) => ({
                    id: option.id,
                    name: option.name,
                    votes: option.votes
                }))
            }));
        } catch (error) {
            console.error('Error fetching polls:', error);
            return [];
        }
    },
    vote: async (pollId, optionId, voterEmail) => {
        try {
            console.log('Sending vote:', { pollId, optionId, voterEmail });
            const response = await axios.post(`http://localhost:5248/api/v1/Poll/${pollId}/votes`, {
                optionId: optionId,
                emailVoter: voterEmail
            });
            return response.data;
        } catch (error) {
            console.error('Vote error:', error.response?.data);
            throw new Error(error.response?.data?.details || 'Error submitting vote');
        }
    }
};