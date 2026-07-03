type MessageProps = {
  name: string;
};

function Message({ name }: MessageProps) {
  return <h2>Welcome {name}!</h2>;
}

export default Message;