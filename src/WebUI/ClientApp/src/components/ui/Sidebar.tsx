import { Routes } from '@/types/enums'
import React from 'react'
import { Link } from 'react-router-dom'

const Sidebar = () => {
  return (
    <div className="w-1/5 border-r-2">
      <ul className="flex flex-col justify-center">
        <li className="px-2">
          <Link to={Routes.Home}>Home</Link>
        </li>
        <li className="px-2">
          <Link to={Routes.Devices}>Devices</Link>
        </li>
        <li className="px-2">
          <Link to={Routes.Groups}>Groups</Link>
        </li>
      </ul>
    </div>
  )
}

export default Sidebar
