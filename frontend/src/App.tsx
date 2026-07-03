import Message from "./Message";
import { useState } from "react";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h1>Internship Demo</h1>
      <Message name="Özge" />

      <p>Today I learned:</p>

      <ul style={{ listStyle: "none", padding: 0 }}>
        <li>✅ React</li>
        <li>✅ TypeScript</li>
        <li>✅ Vite</li>
      </ul>

      <h3>You clicked {count} times.</h3>

      <button onClick={() => setCount(count + 1)}>
        Click Me
      </button>
    </div>
  );
}

export default App;