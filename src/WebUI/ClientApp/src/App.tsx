import React, { useState } from 'react'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div className="h-screen w-screen ">
      <div className="bg-blue-400 h-full w-full flex flex-col justify-center items-center">
        <p>Hello Vite + React!</p>
        <p>
          <button type="button" onClick={() => setCount((count) => count + 1)}>
            count is: {count}
          </button>
        </p>
        <p>
          Edit <code>App.tsx</code> and save to test HMR updates.
        </p>
      </div>
    </div>
  )
}

export default App
