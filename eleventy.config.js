import { eleventyImageTransformPlugin } from "@11ty/eleventy-img";

export default async function(eleventyConfig) {

  eleventyConfig.addPassthroughCopy({
    "./public/": "/"
  });

  eleventyConfig.addPlugin(eleventyImageTransformPlugin, {
      formatFiltering: ["animated"],
      sharpOptions: {
        animated: true
      }
    }
  )

};

export const config = {
  dir: {
    input: "content"
  }
}
