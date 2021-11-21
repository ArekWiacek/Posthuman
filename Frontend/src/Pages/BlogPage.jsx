import React, { useState, useEffect } from 'react';
import { Grid } from '@mui/material';
import BlogPost from '../components/Blog/BlogPost';
import Api from '../Utilities/ApiHelper';

const post1 = {
    id: 1,
    imageUrl: "/Assets/Images/BlogPosts/cyborg.png",
    title: "Version 0.1 released",
    subtitle: "We are officially going online, bitches! Whoop whoop!",
    sections: [
        {
            title: "Todo Page",
            features: [
                "Use todo page to keep track of your everyday plans",
                "Create, edit or delete tasks",
                "Create inline subtasks fast and easy way (Enter and Escape buttons are your friends)",
                "Nest tasks inside each other to create hierarchy of more complex goals",
                "See the progress of your activities",
                "Change display options to fit your needs"
            ]
        },
        {
            title: "Avatar Page",
            features: [
                "See your Avatar - virtual representation of yourself",
                "Gain experience points by completing tasks",
                "Reach new levels both in game and life"
            ]
        },
        {
            title: "Other",
            features: [
                "Responsive layout",
                "Material UI integration"
            ]
        }]
};

const post2 = {
    imageUrl: "/Assets/Images/BlogPosts/pcb1.jpg",
    title: "Version 0.2 released",
    subtitle: "Holy shit, new version is here!",
    sections: [
        {
            title: "Todo page",
            features: [
                "Display options to manage tasks visibility: show / hide finished, visibility on / off",
                "Tasks sorting",
                "Collapsible actions menu",
                "Correct displaying on every screen size"
            ]
        },
        {
            title: "Real time notifications",
            features: [
                "SignalR integrated",
                "Notifications panel with messages from server"
            ]
        },
        {
            title: "Other",
            features: [
                "Updated game mechanics",
                "Randomness introduced",
                "Avatar now visible also on todo page"
            ]
        }]
};

const BlogPage = () => {
    const blogPostsEndpointName = "BlogPosts";
    const [blogPosts, setBlogPosts] = useState([]); 

    
    useEffect(() => {
        Api.Get(blogPostsEndpointName, posts => {
            setBlogPosts(posts);
        });
    }, []);

    return (
        <Grid container spacing={3}>
            {
                blogPosts.map(post => (
                    <Grid item xs={12} md={6} lg={4} key={post.id}>
                        <BlogPost post={post}>
                            
                        </BlogPost>
                    </Grid>
                ))
            }
        </Grid>
    );
}

export default BlogPage;