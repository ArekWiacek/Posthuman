import React, { useState, useEffect } from 'react';
import { Grid } from '@mui/material';
import BlogPost from '../components/Blog/BlogPost';
import Api from '../Utilities/ApiHelper';

const LandingPage = () => {
    useEffect(() => {    
    }, []);

    return (
        <Grid container spacing={3}>
            {
                blogPosts.map(post => (
                    <Grid item xs={12} md={6} lg={4} key={post.id} sx={{ display: !post.isPublished ? 'none' : '' }}>
                        <BlogPost post={post}></BlogPost>
                    </Grid>
                ))
            }
        </Grid>
    );
}

export default LandingPage;