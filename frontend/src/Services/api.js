import axios from 'axios';

export const pollService = {
    getPolls : async () => {
        try {
            const response = await axios.get('http://localhost:5248/api/v1/Poll');
            return response.data.map((poll) => ({
                name: poll.name,
                options: poll.options.map(option => ({
                    name: option.name,
                    votes: option.votes
                }))

            }));
        } catch (error) {
            console.error('Error fetching books:', error);
            return [];
        }
    }
}