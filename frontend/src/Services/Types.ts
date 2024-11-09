export interface PollOption {
    id: string;
    name: string;
    votes: number;
}

export interface Poll {
    id: string;
    name: string;
    options: PollOption[];
}
