import Navbar from '@/components/ui/Navbar'
import React from 'react'
import { ReactNode } from 'react'
import Sidebar from '../ui/Sidebar'

interface LayoutProps {
    children?: ReactNode;
}

const Layout: React.FC<LayoutProps> = props => {
    return (
        <div className="bg-white h-screen flex flex-row">
            <Sidebar />
            <div className="flex flex-col w-screen">
                <Navbar />
                <div className="p-4">
                    {props.children}
                </div>
            </div>
        </div>
    )
}

export default Layout
