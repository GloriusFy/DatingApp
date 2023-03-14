export interface ThreadMessage {
    id: number,
    senderId: number,
    recipientId: number,
    content: string,
    readDate?: Date,
    sendDate: Date
}